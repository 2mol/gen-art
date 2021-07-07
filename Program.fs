open System.IO
open System.Text

open SharpVG
open FsRandom

let rectSize = 32
let nRectsX = 12
let nRectsY = 24
let nRects = nRectsX * nRectsY
let margin = 1
let canvasArea =
  { Width = Length.ofInt (rectSize * 12 + 2 * margin)
    Height = Length.ofInt (rectSize * 24 + 2 * margin)
  }

let randomState = createState xorshift (123456789u, 362436069u, 521288629u, 88675123u)

let rectangle n =
  random {
    let! offSetX = ``[0, 1)``
    let! offSetY = ``[0, 1)``
    // let! offSetY = Statistics.normal (0., 10. * (float n))
    let style = Style.create (Color.ofName White) (Color.ofName Black) (Length.ofInt 1) 1.0 0.0
    let pureX = rectSize * (n % nRectsX) + margin |> float
    let pureY = rectSize * (n / nRectsX) + margin |> float
    let position = Point.ofFloats (pureX + (20.*offSetX), pureY + (20.*offSetY))
    let area = Area.ofInts (rectSize, rectSize)

    let element =
        Rect.create position area
        |> Element.createWithStyle style

    return element
  }

[<EntryPoint>]
let main _ =
  let drawing =
    {0 .. nRects - 1}
    |> Seq.map (fun n -> Random.get (rectangle n) randomState)
    |> Group.ofSeq
    // |> Group.addElements
    |> Svg.ofGroup
    |> Svg.withSize canvasArea

  // let html =
  //     Svg.toHtml "Drawing" drawing

  // File.WriteAllText("index.html", html, Encoding.UTF8)
  File.WriteAllText("out.svg", drawing.ToString(), Encoding.UTF8)

  0
