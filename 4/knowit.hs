module Main where

data Dir   = North | East deriving (Show, Eq)
data State = State { x :: Int, y :: Int, dir :: Dir } deriving (Show)

progress :: State -> State
progress state = case dir state of
  North ->
    case remsRow of
      [True, False] -> state { y = y state + 1, dir = East } 
      _             -> state { y = y state + 1 } 
  East ->
    case remsCol of
      [False, True] -> state { x = x state + 1, dir = North } 
      _             -> state { x = x state + 1 } 
  where
    remsRow = map (== 0) [(1+y state) `rem` 3, (1+y state) `rem` 5]
    remsCol = map (== 0) [(1+x state) `rem` 3, (1+x state) `rem` 5]

initialState :: State
initialState = State { x = 0, y = 0, dir = North }

main :: IO ()
main = print "ok"
