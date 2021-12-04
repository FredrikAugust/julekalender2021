{-# LANGUAGE TypeApplications #-}

module MyLib (solve) where

import Data.List (find, transpose)
import Data.List.Split (split, splitOn)
import Data.Maybe (fromMaybe, mapMaybe)
import qualified Data.Text as T
import Debug.Trace (trace)
import qualified GHC.Real as T

data Bingo = Bingo
  { boards :: [[[Int]]],
    series :: [Int],
    playedNumbers :: [Int]
  }
  deriving (Show)

parseFile :: T.Text -> Bingo
parseFile file =
  Bingo
    { boards = boards,
      series = map read $ splitOn (",") (series),
      playedNumbers = []
    }
  where
    ls = lines (T.unpack file)
    series = head ls
    rawBoards = map (splitOn "\n") . splitOn "\n\n" . unlines $ drop 2 ls
    boards = map parseBoard rawBoards

parseBoard :: [String] -> [[Int]]
parseBoard rawBoard =
  filter (/= []) . map (map (read @Int) . filter (/= "") . splitOn " ") $ rawBoard

hasBingo :: [[Int]] -> [Int] -> Bool
hasBingo board series = any (all (`elem` series)) $ (board <> (transpose $ board))

runUntilBingo :: Bingo -> (Maybe [[Int]], [Int])
runUntilBingo game = case firstBingo of
  Nothing -> (Nothing, [])
  Just bi -> trace (show score') (find ((flip hasBingo) (playedNumbers bi)) $ boards bi, playedNumbers bi)
  where
    bingos = scanl (\game num -> game {playedNumbers = (playedNumbers game) <> [num]}) game (series game)
    firstBingo = find (\game' -> any (\board' -> hasBingo board' (playedNumbers game')) $ boards game') bingos
    lastBingoBreak = break (\game' -> length (filter ((flip hasBingo) $ playedNumbers game') $ boards game') == (length $ boards game')) $ bingos
    lastBingoBefore = last $ fst lastBingoBreak
    lastBingoBefore' = find (\board -> not $ hasBingo board (playedNumbers lastBingoBefore)) $ boards lastBingoBefore
    lastBingoAfter = head $ snd lastBingoBreak
    score' = score (lastBingoBefore', (playedNumbers lastBingoAfter))

score :: (Maybe [[Int]], [Int]) -> Int
score (board, played) = case board of
  Nothing -> 0
  Just board' -> last played * sum (filter (not . (`elem` played)) $ concat $ board')

run :: Bingo -> IO ()
run bingo = print . score $ runUntilBingo bingo

solve :: IO ()
solve = (run . parseFile . T.pack) =<< (readFile "src/input.txt")
