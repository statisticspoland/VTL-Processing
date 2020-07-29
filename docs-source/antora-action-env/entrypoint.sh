#!/bin/sh -l

sh -c "Generating "

sh -c "antora ./docs-source/site.yml"

sh -c "chmod a+xrw build/site"