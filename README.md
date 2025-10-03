
this is a simple application to search for job ads among famous persian websites.

TODOs:
- [x] migrate to angular/.net
    - [x] init project architecture
    - [x] add PrimeNG for ui components
    - [x] re-implement previous features
    - [x] fix loading
- [x] bookmarking
    - [x] service logic to store bookmarks (local storage for now)
    - [x] add route caching in client side
    - [x] sync bookmark sign between search and bookmark page
    - [x] fix bookmark indicator sync when caching router
    - [x] actually store bookmarks (requires persistence and user authentication)
- [x] simple authentication with jwt tokens (persistence with efcore+mysql)
- [ ] show toasts
    - [ ] when bookmarked
    - [ ] empty query
    - [ ] errors
- [x] choose search source
- [ ] add rate limit
- [x] show ad description modal
- [ ] stats api
- [ ] dockerize and deploy
- [ ] parsers
    - [x] jobinja
    - [x] quera
    - [x] jobvision
    - [ ] irantalent
    - [ ] e-estekhdam
    - [ ] linkedin (if it's possible)
    - [ ] divar
    - [ ] ponisha
    - [ ] parscoders
    - [ ] ...