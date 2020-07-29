#!/bin/sh -l

sh -c "Generating "

sh -c "antora site.yml"

sh -c "chmod a+xrw build/site"