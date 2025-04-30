---

## 🔹 GET: `GetAllPersons`
**Beskrivning:** Hämtar alla personer i systemet.

**Respons:**
- `200 OK` – En lista av `PersonDTO`.

---

## 🔹 GET: `GetPersonById`
**Beskrivning:** Hämtar en enskild person med ett specifikt ID.

**Parametrar:**
- `personId` *(int)* – ID för personen.

**Respons:**
- `200 OK` – En `PersonDTO`.
- `404 Not Found` – Om personen inte hittades.

---

## 🔹 GET: `GetInterestsForPerson`
**Beskrivning:** Hämtar alla intressen kopplade till en specifik person.

**Respons:**
- `200 OK` – Lista av `InterestDTO`.
- `404 Not Found` – Om inga intressen hittades.

---

## 🔹 GET: `GetLinksForPerson`
**Beskrivning:** Hämtar alla länkar kopplade till en specifik person.

**Respons:**
- `200 OK` – Lista av `LinkDTO`.
- `404 Not Found` – Om inga länkar hittades.

---

## 🔸 POST: `AddInterestToPerson`
**Beskrivning:** Kopplar ett befintligt intresse till en specifik person.

**Parametrar:**
- `personId` *(int)*
- `interestId` *(int)*

**Respons:**
- `204 No Content`

---

## 🔸 POST: `AddLink`
**Beskrivning:** Lägger till en ny länk för en specifik person och intresse.

**Body:**
```json
{
  "url": "https://example.com",
  "personId": 1,
  "interestId": 2
}
