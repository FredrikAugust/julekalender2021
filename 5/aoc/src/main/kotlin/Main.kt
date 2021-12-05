fun main() {
    val lines = ((object {}.javaClass::getResource)("input.txt"))?.readText()?.split("\n") ?: return

    val points = lines
        .map { expandLineSegment(it) }
        .filter { it.isNotEmpty() } // Part 1
        .flatten()

    val score = emptyMap<Pair<Int, Int>, Int>().toMutableMap()

    for (point in points) {
        score[point] = score.getOrDefault(point, 0) + 1
    }

    println(score.entries.filter { it.value >= 2 }.size)
}

fun expandLineSegment(input: String): List<Pair<Int, Int>> {
    val startStop = input.split(" -> ")
    val start = startStop.first().split(',').map { it.toInt() }
    val end = startStop.last().split(',').map { it.toInt() }

    val xRange = generateRange(start[0], end[0])
    val yRange = generateRange(start[1], end[1])

    // Part 1: Only consider horizontal/diagonal
    if (!(xRange.size == 1 || yRange.size == 1)) {
        println()
        return xRange.zip(yRange)
    }

    return if (xRange.size == 1) {
        yRange.map { Pair(xRange[0], it) }
    } else {
        xRange.map { Pair(it, yRange[0]) }
    }
}

fun generateRange(from: Int, to: Int): List<Int> {
    if (from > to) {
        return (to .. from).toList().reversed()
    }

    return (from .. to).toList()
}