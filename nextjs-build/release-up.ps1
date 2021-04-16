docker pull davidstahl97/testframework-webapi

docker-compose -f docker-compose.yml -f docker-compose.testframework.yml -f docker-compose.testframework.release.yml -f docker-compose.volumes.yml -f docker-compose.testframework.volumes.yml up -d