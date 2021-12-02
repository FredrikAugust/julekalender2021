package main

import (
	"encoding/csv"
	"fmt"
	"github.com/umahmood/haversine"
	"io"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

type City struct {
	Coordinates haversine.Coord
	Name        string
}

type Curr struct {
	Pos               haversine.Coord
	DistanceTravelled float64
}

func main() {
	dat, _ := os.ReadFile("cities.csv")

	text := strings.TrimSpace(string(dat))

	r := csv.NewReader(strings.NewReader(text))

	state := Curr{
		Pos: haversine.Coord{
			Lat: 90,
			Lon: 135,
		},
		DistanceTravelled: 0.0,
	}

	cities := make([]City, 0)

	for {
		record, err := r.Read()

		if err == io.EOF {
			break
		}

		if err != nil {
			log.Fatal(err)
		}

		city := record[0]
		rawPoint := record[1]

		coordinates := strings.Split(rawPoint[6:len(rawPoint)-2], " ")
		lon, _ := strconv.ParseFloat(coordinates[0], 64)
		lat, _ := strconv.ParseFloat(coordinates[1], 64)

		cities = append(cities, City{
			Coordinates: haversine.Coord{
				Lon: lon,
				Lat: lat,
			},
			Name: city,
		})
	}

	for {
		if len(cities) == 0 {
			break
		}

		sort.Slice(cities, func(i, j int) bool {
			_, iDistance := haversine.Distance(state.Pos, cities[i].Coordinates)
			_, jDistance := haversine.Distance(state.Pos, cities[j].Coordinates)

			return iDistance < jDistance
		})

		_, km := haversine.Distance(state.Pos, cities[0].Coordinates)

		state.DistanceTravelled += km
		state.Pos = cities[0].Coordinates

		cities = cities[1:]

		println("Remaining:", len(cities), "Travelled:", km)
	}

	_, lastKm := haversine.Distance(state.Pos, haversine.Coord{
		Lat: 90,
		Lon: 135,
	})

	state.DistanceTravelled += lastKm

	println(fmt.Sprintf("%.2f", state.DistanceTravelled))

	// svaret er feil
}
