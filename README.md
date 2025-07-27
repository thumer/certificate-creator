# Certificate Creator

Dieses Projekt stellt ein kleines WPF Tool bereit, mit dem selbst signierte Zertifikate erzeugt werden können. Neben der grafischen Oberfläche lässt sich die Anwendung auch über die Kommandozeile verwenden.

## Projektstruktur

- `certificate-creator.sln` - Visual Studio Solution
- `src/CertificateCreator` - WPF Anwendung und Kommandozeilen Tool

## UI Verwendung

Beim Start ohne Parameter öffnet sich die WPF Applikation. Dort können alle Zertifikatsparameter eingegeben werden. Land und Region sind bereits mit `AT` und `Oberösterreich` vorbelegt. Das Ablaufdatum ist standardmäßig auf heute + 5 Jahre gesetzt. Optional kann ein Häkchen gesetzt werden um gleichzeitig ein Root Zertifikat zu erstellen.

Nach Klick auf **Erstellen** wählt man einen Zielordner, in dem die PEM Dateien abgelegt werden.

## Kommandozeile

Die Anwendung kann mit Parametern direkt von der Kommandozeile gestartet werden. Beispiel:

```bash
CertificateCreator.exe --cn=myserver.local --org=Firma --locality=Stadt --output=C:\\Temp --with-root
```

Verfügbare Parameter:

- `--country=<Länderkode>` (Standard `AT`)
- `--state=<Region>` (Standard `Oberösterreich`)
- `--locality=<Ort>`
- `--org=<Organisation>`
- `--cn=<CommonName>`
- `--output=<Zielordner>`
- `--years=<Gültigkeit in Jahren>` (Standard 5)
- `--with-root` Root Zertifikat zusätzlich erstellen

## Build

```bash
dotnet build certificate-creator.sln
```

Benötigt wird das **.NET 9 SDK**.

Beachten Sie, dass das Kompilieren von WPF Anwendungen nur unter Windows möglich ist.
