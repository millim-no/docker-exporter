# Docker exporter

Repository for a Prometheus exporter that exposes information about docker
containers and their CPU and memory usage.

## How to use

```
docker pull ghcr.io/millim-no/docker-exporter
docker run -v /var/run/docker.sock:/var/run/docker.sock
```

## Technical details

The exporter written in F# send HTTP requests to the Docker Engine API,
through Docker's Unix socket. It uses two endpoints:

- `v1.41/containers/json`
- `v1.41/containers/{container_id}/stats?stream=false`

## Troubleshooting

### `/var/run/docker.sock` does not exist
If running MacOS, go to Docker Desktop > Settings > Advanced and check
_Allow the default Docker socket to be used (requires password)_.

## Acknowledgments

Prometheus configuration from [Mirco](https://dev.to/ablx/minimal-prometheus-setup-with-docker-compose-56mp).
