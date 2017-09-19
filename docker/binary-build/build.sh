#!/usr/bin/env bash

#echo on
set -x

docker build -t aspnet-build-tools:binary-build -f Dockerfile ../.. | tee docker-build.log
