#!/bin/sh -l

sh -c "Generating "
sh -c "cd docs-source"
sh -c "antora site.yml"
sh -c "chmod a+xrw build/site"