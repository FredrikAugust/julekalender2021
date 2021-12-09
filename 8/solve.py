number_of_digits = {
    '0': 6,
    '1': 2,
    '2': 5,
    '3': 5,
    '4': 4,
    '5': 5,
    '6': 6,
    '7': 3,
    '8': 7,
    '9': 6,
}

safe_numbers = {
    2: 1,
    3: 7,
    4: 4,
    7: 8
}

def find_and_remove_by_len(n, list):
    for i in list:
        if len(i) == n:
            list.remove(i)
            return i

with open('./input.txt', 'r') as f:
    ls = f.readlines()
    lines = [x.split(' | ')[0].strip() for x in ls]
    codes = [x.split(' | ')[1].strip() for x in ls]

    digit_signal_counts = [list(map(list, y.split(' '))) for y in lines]

    # print(sum([len([safe_numbers[x] for x in y if x in safe_numbers]) for y in digit_signal_counts]))

    nums = []
    for i, signals in enumerate(digit_signal_counts):
        code = codes[i]
        code = list(map(set, code.split(' ')))
        signals = [set(x) for x in signals]
        answer = {}

        answer[1]=set(find_and_remove_by_len(2, signals))
        answer[4]=set(find_and_remove_by_len(4, signals))
        answer[7]=set(find_and_remove_by_len(3, signals))
        answer[8]=set(find_and_remove_by_len(7, signals))

        top = answer[7].difference(answer[1])
        right_side = answer[1]
        top_left_l = answer[4].difference(answer[1]).difference(answer[7])
        bottom_left_l = answer[8].difference(answer[4]).difference(answer[7])

        nine_and_six = [x for x in signals if len(x) == 6 and len(x.intersection(top_left_l)) == 2]
        zero = [x for x in signals if len(x) == 6 and x not in nine_and_six][0]

        answer[0] = zero

        centre = nine_and_six[0].difference(zero)

        nine = [x for x in nine_and_six if len(x.intersection(right_side)) == 2][0]
        answer[9] = nine

        six = [x for x in nine_and_six if x != nine][0]
        answer[6] = six

        bottom_left = answer[8].difference(nine)
        bottom = bottom_left_l - bottom_left

        three = right_side.union(centre).union(bottom).union(top)
        answer[3] = three

        top_right = answer[8].difference(six)

        two = top_right.union(top).union(bottom).union(centre).union(bottom_left)
        answer[2] = two

        five = answer[8].difference(answer[2]).union(centre).union(top).union(bottom)
        answer[5] = five

        items = answer.items()

        num = []
        for c in code:
            for k, v in items:
                if v == c:
                    num.append(str(k))
                    break
        nums.append(int(''.join(num)))

    print(sum(nums))



"""
be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
1  8       9      6      4          0      3     2     7

  
be = 1
edb = 7
cgeb = 4
cfbegad = 8


  0:      1:      2:      3:      4:
 aaaa    ....    aaaa    aaaa    ....
b    c  .    c  .    c  .    c  b    c
b    c  .    c  .    c  .    c  b    c
 ....    ....    dddd    dddd    dddd
e    f  .    f  e    .  .    f  .    f
e    f  .    f  e    .  .    f  .    f
 gggg    ....    gggg    gggg    ....

  5:      6:      7:      8:      9:
 aaaa    aaaa    aaaa    aaaa    aaaa
b    .  b    .  .    c  b    c  b    c
b    .  b    .  .    c  b    c  b    c
 dddd    dddd    ....    dddd    dddd
.    f  e    f  .    f  e    f  .    f
.    f  e    f  .    f  e    f  .    f
 gggg    gggg    ....    gggg    gggg
"""