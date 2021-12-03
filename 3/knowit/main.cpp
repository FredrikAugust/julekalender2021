#include <iostream>
#include <fstream>
#include <vector>

int main() {
    std::ifstream file_stream;

    std::string line;

    file_stream.open("/Users/fredrik-alv/IdeaProjects/julekalender2021/3/input.txt");

    // only one line
    getline(file_stream, line);
    file_stream.close();

    int longest = -1;
    int start_index = -1;

    for (int i = 0; i < line.length(); i++) {
        int balance = 0;
        std::vector<char> seq = std::vector<char>();

        for (int j = i; j < line.length(); j++) {
            char c = line[j];

            if (c == 'J')
                balance += 1;
            else
                balance -= 1;

            seq.push_back(c);

            if (balance == 0) {
                int score = (int) seq.size();
                if (score > longest) {
                    longest = score;
                    start_index = i;
                }
            }
        }
    }

    printf("Longest: %d from index %d", longest, start_index);

    return 0;
}
