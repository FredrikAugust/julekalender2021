module Main where

import qualified MyLib (solve)

main :: IO ()
main = do
  putStrLn "Hello, Haskell!"
  MyLib.solve
