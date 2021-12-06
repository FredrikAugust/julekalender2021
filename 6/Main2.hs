--- Day 6: Runs in 0.02 secs in GHCi
{-# LANGUAGE TypeApplications #-}

module Main2 where

import Data.List (group, sort)
import qualified Data.Map as M

-- |
-- A primitive split function written by myself without google
--
-- >>> splitOn ',' "12,32,431,12"
-- NOW ["12","32","431","12"]
splitOn :: Char -> String -> [String]
splitOn _ "" = []
splitOn c str = filter (/= "") $ match : splitOn c (drop (max 1 (length match)) str)
  where
    match = (takeWhile (/= c) str)

-- |
-- Parse list of integers into occurence count map
parseToMap :: [String] -> M.Map Int Int
parseToMap =
  M.fromList
    . map (\nums -> (head nums, length nums))
    . group
    . sort
    . map (read @Int)

-- |
-- Outcome of a fish based on life
tick :: (Int, Int) -> [(Int, Int)]
tick (0, x) = [(6, x), (8, x)]
tick (n, x) = [(n - 1, x)]

-- |
-- Aggregate new states into map, making sure
-- to respect already existing state
progress :: M.Map Int Int -> M.Map Int Int
progress m =
  foldl
    (\acc curr -> M.insertWith (+) (fst curr) (snd curr) acc)
    M.empty
    (concatMap tick $ M.assocs m)

main :: IO ()
main =
  print
    . sum
    . map snd
    . M.toList
    . head
    . drop 256
    . iterate progress
    . parseToMap
    . splitOn ','
    =<< readFile "input.txt"
