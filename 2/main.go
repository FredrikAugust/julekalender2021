package main

import (
	"os"
	"strconv"
	"strings"
)

type Position struct {
	Depth      int
	Horizontal int

	Aim int
}

func main() {
	dat, _ := os.ReadFile("input")

	inputNums := strings.Split(strings.TrimSpace(string(dat)), "\n")

	state := Position{
		Depth:      0,
		Horizontal: 0,
		Aim:        0,
	}

	for _, s := range inputNums {
		segments := strings.Split(s, " ")
		direction := segments[0]
		amount, _ := strconv.Atoi(segments[1])

		switch direction {
		case "down":
			state.Aim += amount
			break
		case "up":
			state.Aim -= amount
			break
		case "forward":
			state.Horizontal += amount
			state.Depth += amount * state.Aim
			break

		}
	}

	print(state.Depth * state.Horizontal)
}
