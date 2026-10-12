# Brain Challenge

A Unity puzzle game that tests memory, reflexes, and number recognition. Players find numbers in the correct order while avoiding X tiles.

## Play Online

[Play the WebGL build](https://sreejithhariharan.github.io/Brain-Challenge/)

## How to Play

1. [Describe the goal, for example: "Tap the numbers in ascending order."]
2. [Describe the X tiles, for example: "Tapping an X tile ends the round or costs a life."]
3. [Describe the scoring or win condition.]

**Controls:** [for example: mouse click or touch]

## Run the WebGL Build Locally

The browser build is in `Build/WebGL`. Browsers can't load it from a file path, so serve it with a local web server:

```bash
cd Build/WebGL
python -m http.server 8000
```

Then open `http://localhost:8000` in Chrome. Click once on the page before playing so the audio can start.

## Features

- [Feature 1, for example: number-order challenges]
- [Feature 2, for example: X tile avoidance]
- [Feature 3, for example: timer or lives]

## Approach

- [Explain the main design, for example: how the number order is generated and checked.]
- [Explain how the game is structured, for example: one manager script controls rounds, and tile scripts handle clicks.]
- Built with Unity 6 and C#. The WebGL build is compressed off so it runs on GitHub Pages without special server headers.

## Project Structure

```
Assets/       Scripts, scenes, prefabs, and art
Build/WebGL/  Browser build
docs/         Copy of the build served by GitHub Pages
```
