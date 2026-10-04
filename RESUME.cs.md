---
schema_version: 11
type: other
category_override: none
file_count: 763
file_extensions: cs:752, csproj:11, json:5, noext:3, txt:3, config:2, md:2, nuspec:1, slnx:1, xml:1, xproj:1
file_extensions_updated: 2026-10-04
avg_lines_per_file: 95
total_lines: 70888
metrics_lm: 2026-10-04 16:02:36
move_to_legacy_percent: 20
description_updated: 2026-10-04
links_updated: 2026-10-04
github_source_url: not run
origin_status: failed
origin_checked: 2026-10-04
article_source_url: not run
article_status: pending
article_checked: not run
last_build_ok: not run
last_build_date: not run
last_tests_run_date: not run
covered_lines: not run
---

## Description

Sbírka kopií cizích knihoven bez doloženého původu, například CommandLineParser, ExCSS, FluentFtp, FlvExtract, FubuCsProjFile, GoogleTranslateFreeApi, SlnGen.Common a SunamoGeoTools, a tři knihovny Metaproject.PackageIndex.* od autora cdorst pro parsování .csproj. Řešení všech projektů je sunamo.notmine.unknownoriginal.slnx. Slouží jako zdrojáky, které nejsou k dispozici jako balíčky.

## Původ zdrojáků

Staženo z GitHubu: **nezjištěno** — pokus o ověření původu selhal.

- Ověřeno: Složka nemá git, původ zdrojů se nepodařilo doložit.

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **20 %** — Kopie cizího kódu bez gitu a bez doloženého zdroje.

- Původ jednotlivých knihoven není ověřen.

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: `SunamoExceptions` (PackageReference), `SunamoShared` (PackageReference), `cl` (ProjectReference, cíl chybí)
