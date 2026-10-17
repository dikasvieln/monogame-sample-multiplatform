# MonoGame 2D Example

A small cross-platform 2D game built with [MonoGame](https://monogame.net/) (DesktopGL) and C#. It runs on Windows, macOS and Linux from the command line with a single script: `./run.sh`.

## Features

- 2D sprite rendering with a sprite sheet
- Keyboard input and a fixed-timestep game loop
- Cross-platform via MonoGame DesktopGL (OpenGL)
- One-command build and run through `run.sh`

## Requirements

| Tool | Version | Notes |
| --- | --- | --- |
| [.NET SDK](https://dotnet.microsoft.com/download) | 8.0 or newer | Required on all platforms |
| Bash or Zsh | any | macOS and Linux have it; on Windows use WSL or Git Bash |
| OpenGL 3.0+ drivers | - | Needed by DesktopGL |

Linux may also need SDL2 and OpenAL:

```bash
# Debian / Ubuntu
sudo apt install libsdl2-2.0-0 libopenal1
```

## Quick Start

```bash
git clone <your-repo-url>
cd <your-repo>

chmod +x run.sh   # first time only
./run.sh
```

## Running with `./run.sh`

```bash
./run.sh                 # build and run (Debug)
./run.sh release         # build and run (Release)
./run.sh clean           # remove bin/ and obj/
./run.sh publish linux   # publish self-contained build (linux | osx | win)
./run.sh help            # show usage
```

### Example `run.sh`

```bash
#!/usr/bin/env bash
set -euo pipefail

PROJECT="MyGame/MyGame.csproj"
CMD="${1:-debug}"

case "$CMD" in
  debug)
    dotnet run --project "$PROJECT" -c Debug
    ;;
  release)
    dotnet run --project "$PROJECT" -c Release
    ;;
  clean)
    dotnet clean "$PROJECT"
    rm -rf MyGame/bin MyGame/obj
    ;;
  publish)
    TARGET="${2:-linux}"
    case "$TARGET" in
      linux) RID="linux-x64" ;;
      osx)   RID="osx-arm64" ;;
      win)   RID="win-x64" ;;
      *) echo "Unknown target: $TARGET"; exit 1 ;;
    esac
    dotnet publish "$PROJECT" -c Release -r "$RID" --self-contained \
      -o "dist/$RID"
    echo "Published to dist/$RID"
    ;;
  help|-h|--help)
    echo "Usage: ./run.sh [debug|release|clean|publish <linux|osx|win>]"
    ;;
  *)
    echo "Unknown command: $CMD"
    echo "Usage: ./run.sh [debug|release|clean|publish <linux|osx|win>]"
    exit 1
    ;;
esac
```

## Controls

| Key | Action |
| --- | --- |
| Arrow keys / WASD | Move |
| Space | Action |
| Esc | Quit |

## Project Structure

```
.
├── run.sh                 # build and run CLI
├── MyGame/
│   ├── MyGame.csproj
│   ├── Program.cs         # entry point
│   ├── Game1.cs           # game loop (Update / Draw)
│   └── Content/
│       ├── Content.mgcb   # MonoGame content pipeline
│       └── Sprites/       # sprite sheets
└── README.md
```

## Creating This Project from Scratch

```bash
dotnet new install MonoGame.Templates.CSharp
dotnet new mgdesktopgl -n MyGame
cd MyGame
dotnet run
```

## Troubleshooting

- **`permission denied: ./run.sh`**: run `chmod +x run.sh`.
- **`dotnet: command not found`**: install the .NET SDK and restart your terminal.
- **Black window or crash on Linux**: update your GPU drivers and confirm SDL2 is installed.
- **Content pipeline errors**: run `dotnet tool restore` so the `mgcb` tool is available.

## Contributing

Contributions, improvements and bug reports are welcome. Whether you're fixing a typo, polishing the gameplay or adding a feature, you're invited to help.

### Open an issue

Found a bug or have an idea? [Open an issue](../../issues) and include:

- **Bug reports:** your OS, .NET version (`dotnet --version`), steps to reproduce, what you expected and what happened, plus any console output or screenshots.
- **Feature requests:** what you'd like to see and why it would help.
- **Questions:** feel free to ask, no question is too small.

Issues labeled `good first issue` or `help wanted` are a great place to start.

### Submit a pull request

1. Fork the repository and create a branch:
   ```bash
   git checkout -b feature/my-improvement
   ```
2. Make your changes and confirm the game still builds and runs with `./run.sh`.
3. Keep commits focused and write clear commit messages.
4. Push your branch and open a pull request describing what changed and why. Link the related issue if there is one.

### Guidelines

- Follow the existing code style and naming conventions.
- Keep changes small and focused; open separate PRs for unrelated changes.
- Test on more than one platform when you can, since this project targets Windows, macOS and Linux.
- Only add assets you have the right to use, and credit their source.
- Be kind and respectful to everyone in the community.

### Ideas for improvement

- Sound effects and music
- Gamepad support
- Gameplay features such as levels, enemies or a score system
- Sprite animation and particle effects
- Automated builds and releases with CI for all three platforms
- Unit tests for game logic

## Credits

- **[MonoGame](https://monogame.net/)**: the open-source framework powering this game.
- **MonoGame Sprite Kit**: sprite artwork and assets used in this project. All credit to the original creators. Please see the kit's own page for its license and attribution requirements: `<link to sprite kit source>`.

## License

Code in this repository is released under the MIT License (see `LICENSE`). Third-party assets, including the sprite kit, keep their original licenses.
