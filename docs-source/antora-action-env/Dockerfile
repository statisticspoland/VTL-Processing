FROM node:10

RUN  npm i -g @iarna/toml @antora/cli @antora/site-generator-default asciidoctor-plantuml antora-site-generator-lunr

ADD entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
