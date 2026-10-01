---
schema_version: 6
type: library
file_count: 18
avg_lines_per_file: 24
move_to_legacy_percent: 60
generated_date: 2026-10-01
generated_time: 16:41:12
github_source_url: 
last_build_ok: 
last_build_date: 
last_tests_run_date: 
covered_lines: 
total_lines: 
---

## Description

Tři malé knihovny v C# (.NET 9) pro parsování `.csproj` souborů: `CsprojFileParser`, `CsprojFilesParser` a datové typy `PackageCsproj` a `NuGetPackageInfoFactory`. Kód napsal cizí autor `cdorst` (LICENSE odkazuje na jeho GitHub, balíčky mají prefix `CDorst.`). Repo drží jen zdrojáky bez testů a bez dokumentace.

## Původ zdrojáků

Staženo z GitHubu: **ne** — původní autor je `cdorst`, ale jeho zdrojový repo se na GitHubu nepodařilo dohledat, takže zdroj nejde doložit.
- Ověřeno: LICENSE `Z https://github.com/cdorst`, `Authors=cdorst` a `PackageId=CDorst.Metaproject...` v csproj; účet `cdorst` má 0 veřejných repozitářů. `gh search repos` na "Metaproject.PackageIndex", "cdorst metaproject", "PackageIndex ParseCsprojFile" a `gh search code` na jmenný prostor vrátily jen toto repo (`sunamo/sunamo.notmine.unknownoriginal`), bez hash shody ke kterémukoli cizímu repu. Historie (od 2023-11) má jen autory sunamo.

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **60 %** — malý cizí kód s neověřitelným původem, bez využití jinde.
- Repo obsahuje jen 3 drobné projekty a poznámka `_.txt` hodnotí kvalitu kódu jako špatnou.
- Kód není vlastní a jeho zdroj nelze dohledat, ostatní projekty z původního `notmine` už byly odstraněny.
- Poslední commity jsou jen úklid a přidání RESUME.

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: žádné
