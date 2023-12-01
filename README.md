## Sistemos paskirtis
Automatizavimo programų pardavimo portalas. 

Sistema skirta palengvinti Automatizavimo programų pardavimus. Pardavėjas gali sukurti parduotuvę ir sukurti joje parduodamas programas. Vartotojas gali užsisakyti prenumeratą šios programos.

## Funkciniai reikalavimai
## Svečias gali:
    Prisiregistruti
        Kaip vartotojas
        Kaip pardavėjas
    Prisijungti
Pardavėjas gali:
    • Sukurti parduotuvę
    • Sukurti pardudamą programą
        o Programa gali būti ir nemokama
    • Matyti savo parduotuves
    • Matyti savo parduotuvės programas
    • Redaguoti programą
    • Redaguoti parduotuvę

Vartotojas gali:
    • Matyti visas parduotuves
    • Matyti parduotuvės parduodamas programas
    • Pasirinkti parduodamą program
        o Įdėti/Išimti prekes į krepšelį
        o Prenumeruoti programą
    • Matyti savo prenumeratas
        o Redaguoti prenumeratą
    ▪ Keisti terminą
    ▪ Istrinti prenumeratą

Administratrius gali:
• Matyti visas programas
    o Ištrinti programą
• Matyti visas parduotuves
    o Ištrinti parduotuvę

## Sistems architektūra

    Backend - .NET 7, EF Core, PostgreSQL

    Frontend – React Vite Typescript

    Deployment - Docker Compose, NGINX, Ubuntu, Neon
    
## Deplyment diagrama


![uml](./diagrams/uml.png)
