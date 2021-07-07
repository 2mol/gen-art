open System.IO
open System.Text

open SharpVG

let rectSize = 32
let nRectsX = 12
let nRectsY = 24
let nRects = nRectsX * nRectsY
let margin = 1
let canvasArea =
  { Width = Length.ofInt (rectSize * 12 + 2 * margin + 30)
    Height = Length.ofInt (rectSize * 24 + 2 * margin + 30)
  }

let random = new System.Random(1)

let rectangle n =
  let offSetX = float (random.Next(0,n)) / 12.
  let offSetY = float (random.Next(0,n)) / 12.
  let style = Style.create (Color.ofName White) (Color.ofName Black) (Length.ofInt 1) 1.0 0.0
  let pureX = rectSize * (n % nRectsX) + margin
  let pureY = rectSize * (n / nRectsX) + margin
  let position = Point.ofFloats (float pureX + offSetX, float pureY + offSetY)
  let area = Area.ofInts (rectSize, rectSize)

  Rect.create position area
  |> Element.createWithStyle style


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
