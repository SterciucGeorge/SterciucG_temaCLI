# Raspuns la intrebari

- Ce este un viewport?
  
Viewport-ul este fereastra unde se intample randarea.
- Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
  Fames per second reprezinta numarul de imagini ce vor fi afisate per secunda pentru a crea iluzia de miscare, mai multe frame-uri rezulta intr-o fluiditate crescuta.
- Când este rulată metoda OnUpdateFrame()?
  Este rulata odata per cadru inainte de OnRenderFrame(), daca am avea 60fps ar fi apelata de 60 de ori intr-o secunda.
- Ce este modul imediat de randare?
  Este un mod vechi de randare, ce se foloseste de comenzile GL.Begin, GL.End si GL.Vertex. Apelurile de desenare sunt facute direct pentru fiecare primitiv, iar datele despre varfuri sunt trimise pentru fiecare cadru, de fiecare data cand se doreste redarea unui obiect.
- Care este ultima versiune de OpenGL care acceptă modul imediat?
  Versiunea 3.0
- Când este rulată metoda OnRenderFrame()? 
  Este rulata la fiecare cadru dupa OnUpdateFrame().
- De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
  Pentru a redimensiona scena grafica in raport cu fereastra de vizualizare. Fara rularea OnResize() imaginea redata ar fi distorsionata sau incorect dimensionata.
- Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
  Primul paramatru este "near clip plane" ce reprezinta cea mai mica distanta la care obiectele vor fi vizibile. Al doilea parametru este "far clip plane" ce reprezinta cea mai mare distanta la care obiectele vor fi vizibile. "Near clip plane" nu poate fi mai mic decat 1 sau mai mare decat "far clip plane", iar "far clip plane" nu poate fi mai mic decat "near clip plane".
