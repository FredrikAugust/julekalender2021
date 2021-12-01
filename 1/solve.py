# with open('./input', 'r') as f:
#     nums = list(map(int, f.readlines()))
#     increasing = 0
#     for i, num in enumerate(nums):
#         if i == 0:
#             continue
#
#         if num > nums[i-1]:
#             increasing += 1
#
#     print(increasing)

with open('./input', 'r') as f:
    nums = list(map(int, f.readlines()))
    increasing = 0
    for i, num in enumerate(nums):
        if i in [0, 1, 2]:
            continue

        window = sum([nums[x] for x in [i-2, i-1, i]])
        prev_window = sum([nums[x] for x in [i-3, i-2, i-1]])

        if window > prev_window:
            increasing += 1

    print(increasing)