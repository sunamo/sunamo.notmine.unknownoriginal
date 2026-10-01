# sunamo.notmine.unknownoriginal

## Short description

Tři malé knihovny v C# (.NET 9) pro parsování `.csproj` souborů: `CsprojFileParser`, `CsprojFilesParser` a datové typy `PackageCsproj` a `NuGetPackageInfoFactory`. Kód napsal cizí autor `cdorst` (LICENSE odkazuje na jeho GitHub, balíčky mají prefix `CDorst.`). Repo drží jen zdrojáky bez testů a bez dokumentace.

Malá sada tří knihoven v C# (.NET 9) pro čtení informací z `.csproj` souborů.
Kód pochází od autora `cdorst` (viz `LICENSE` a `Authors`/`PackageId` v csproj), ne od majitele repa.

## Obsah

- `Metaproject.PackageIndex.Functions.ParseCsprojFile` – `CsprojFileParser`, čte z řádků csproj tagy `Description`, `Version`, `TargetFramework` a reference.
- `Metaproject.PackageIndex.Functions.ParseCsprojFiles` – `CsprojFilesParser`, zpracuje víc csproj najednou.
- `Metaproject.PackageIndex.Structures.PackageProject` – datové typy (`PackageCsproj`, `NuGetPackageInfoFactory`).

## Poznámky

- Řešení: `sunamo.notmine.sln`.
- Projekty se odkazují na balíčky `SunamoExceptions` a `SunamoShared`.
- `_.txt` obsahuje osobní poznámku o kvalitě původního kódu.
