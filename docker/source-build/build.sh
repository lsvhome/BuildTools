#!/usr/bin/env bash

#echo on
set -x

docker build -t aspnet-build-tools:source-build ../.. | tee docker-build.log
