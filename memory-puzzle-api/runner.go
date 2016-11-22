//Author: Peter Nagy - https://github.com/pete314
//Since: 2016.11.08.
//Description: Simple API for storing and retrieving scores for memory puzzle

package main

import (
	"gopkg.in/macaron.v1"
	"flag"
	"log"
	"gopkg.in/mgo.v2"
	"gopkg.in/mgo.v2/bson"
	"time"
	"encoding/json"
)

const(
	DB_COLLECTION = "scores"
)

type Score struct{
	ID        string 	`json:"id" bson:"_id"`
	IsRemote  bool
	Collection string 	`json:"collection" bson:"collection"`
	Username   string	`json:"username" bson:"username"`
	TotalSeconds int	`json:"total_seconds" bson:"total_seconds"`
	Score	int		`json:"score" bson:"score"`
	PuzzleSize int		`json:"puzzlesize" bson:"puzzlesize"`
	Created time.Time	`json:"created" bson:"created"`
}

func main(){
	var (
		mongo = flag.String("mongo", "localhost", "mongodb address")
		dbname  = flag.String("dbname", "memory_puzzle", "database name")
	)
	log.Println("Dialing mongo", *mongo)
	db, err := mgo.Dial( *mongo )
	if err != nil {
		log.Fatalln("Failed to connect to mongo:", err)
	}
	defer db.Close()
	db.DB(*dbname)

	m := macaron.Classic()
	m.Use(macaron.Renderer())

	//Score get endpoint
	m.Get("/score/:collection([\\w]+)", func(ctx *macaron.Context) string {
		collection := (ctx.Params(":collection"))

		results := findScores(db, collection)

		return json.Marshal(results)
	})

	//Score post endpoint
	m.Post("/score", func(ctx *macaron.Context) string {
		body := Score{}
		json.Unmarshal(ctx.Req.Body().String(), body)

		result := storeScore(db, body)

		return json.Marshal(struct{success bool}{result})
	})

	m.Run()
}

//Create Score entry
func storeScore(db *mgo.Database, res *Score) bool{
	c := db.C(DB_COLLECTION)

	if err := c.Insert(res); err == nil {
		return true
	}else{
		return false
	}
}

//Find scores in database
func findScores(db *mgo.Database, collection string ) []*Score{
	c := db.C(DB_COLLECTION)
	var q *mgo.Query
	var result []*Score

	q = c.Find(bson.M{"collection": collection}).Limit(10).Sort("-score")

	if err := q.All(&result); err == nil {
		return result
	}else{
		return nil
	}

}