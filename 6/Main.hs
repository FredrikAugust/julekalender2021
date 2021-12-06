{-# LANGUAGE TypeApplications #-}

module Main where

import qualified Data.Map as M

-- |
-- A primitive split function written by myself without google
splitOn :: Char -> String -> [String]
splitOn _ "" = []
splitOn c str = filter (/= "") $ match : splitOn c (drop (max 1 (length match)) str)
  where
    match = (takeWhile (/= c) str)

parseToInts :: [String] -> [Int]
parseToInts = map (read @Int)

tick :: Int -> [Int]
tick 0 = [6, 8]
tick n = [n - 1]

progress :: [Int] -> [Int]
progress = concat . map tick

life :: [Int] -> [[Int]]
life = iterate progress

main :: IO ()
main =
  mapM_ (print . length)
    . take (256 + 1) -- first is initial state
    . life
    . parseToInts
    . splitOn ','
    =<< readFile "input.txt"