# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 0.0.3 - Unreleased

### Changed
- Report missing memory stats as "-1"

### Fixed
- CPU clock counter value name ends with "_total" to adhere to Prometheus
  exporter specification.
- Missing memory stats do not cause the exporter to fail.

## 0.0.2 - 2024-05-18
- Initial public release

## 0.0.1 - 2024-05-16
- Initial release
- Export CPU and memory
