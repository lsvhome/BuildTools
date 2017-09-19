#!/usr/bin/env bash

#echo on
set -x

docker build -t aspnet-build-tools:binary-build . | tee docker-build.log
