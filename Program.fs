open System.IO
open System.Text

open SharpVG

let rectSize = 32
let nRectsX = 12
let nRectsY = 22
let nRects = nRectsX * nRectsY
let margin = 1
let canvasArea =
  { Width = Length.ofInt (rectSize * nRectsX + 2 * margin + 30)
    Height = Length.ofInt (rectSize * nRectsY + 2 * margin + 30)
  }

let random = new System.Random(0)

let rectangle n =
  let offSetX = float (random.Next(0,n)) / 10.
  let offSetY = float (random.Next(0,n)) / 10.

  let style = Style.create (Color.ofName White) (Color.ofName Black) (Length.ofInt 1) 1.0 0.0
  let area = Area.ofInts (rectSize, rectSize)

  let xPosition =
    rectSize * (n % nRectsX) + margin
    |> float
    |> (+) offSetX

  let yPosition =
    rectSize * (n / nRectsX) + margin
    |> float
    |> (+) offSetY

  let position = Point.ofFloats (xPosition, yPosition)

  let rotationAngle = float n * (random.NextDouble() - 0.5) / 2.5
  let rotation =
    Transform.createRotate rotationAngle
      (Length.ofFloat (xPosition + 16.))
      (Length.ofFloat (yPosition + 16.))

  Rect.create position area
  |> Element.createWithStyle style
  |> Element.withTransform rotation


[<EntryPoint>]
let main _ =
  let drawing =
    {0 .. nRects - 1}
    |> Seq.map rectangle
    |> Group.ofSeq
    // |> Group.addElements
    |> Svg.ofGroup
    |> Svg.withSize canvasArea

  // let html =
  //     Svg.toHtml "Drawing" drawing

  // File.WriteAllText("index.html", html, Encoding.UTF8)
  File.WriteAllText("out.svg", drawing.ToString(), Encoding.UTF8)

  0
