#!/bin/bash

cd docs-source
antora site.yml

chmod a+xrw build/site