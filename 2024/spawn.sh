#!/bin/bash -x

for i in $(seq 5 25); do
    cp src/Day_X.cs src/Day_$i.cs
    sed -i "" "s/X/$i/g" src/Day_$i.cs

    cp test/unit/Day_XTest.cs test/unit/Day_${i}Test.cs
    sed -i "" "s/X/$i/g" test/unit/Day_${i}Test.cs
done