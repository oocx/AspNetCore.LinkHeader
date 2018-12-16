[![Build Status](https://dev.azure.com/oocx/Oocx.AspNetCore.LinkHeader/_apis/build/status/oocx.AspNetCore.LinkHeader?branchName=master)](https://dev.azure.com/oocx/Oocx.AspNetCore.LinkHeader/_build/latest?definitionId=2?branchName=master)
# AspNetCore.LinkHeader
Easily generate HTTP preload / prefetch and preconnect link headers. Can be used to initiate HTTP2 server push when combined with Cloudflare or other proxies / CDNs that support preload headers.

This project is work in progress. Next steps:
- create a NuGet package, automatically publish the package after build
- add more tests
- support hash replacement for non-top-level files
- clean up / refactor code
