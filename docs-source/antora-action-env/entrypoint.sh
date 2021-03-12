#!/bin/bash

cd docs-source
DOCSEARCH_ENABLED=true DOCSEARCH_ENGINE=lunr NODE_PATH="$(npm -g root)" antora --generator antora-site-generator-lunr site.yml
chmod a+xrw build/site
