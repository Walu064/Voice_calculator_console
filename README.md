# Voice_calculator_console

## Przegląd

Kalkulator Głosowy to prosta aplikacja konsolowa, która umożliwia użytkownikom wykonywanie podstawowych operacji arytmetycznych za pomocą poleceń głosowych. Aplikacja rozpoznaje wymówione liczby i operatory matematyczne, wykonuje określone obliczenia i udziela wyników przy użyciu syntezy mowy.

## Funkcje

- **Rozpoznawanie Głosu:** Aplikacja używa silnika rozpoznawania mowy Microsoft Speech Recognition do interpretacji mówionych poleceń.
- **Operacje Arytmetyczne:** Użytkownicy mogą wykonywać dodawanie, odejmowanie, mnożenie i dzielenie za pomocą poleceń głosowych.
- **Synteza Mowy:** Aplikacja zapewnia dźwięczne potwierdzenie wprowadzonych poleceń oraz prezentuje wyniki obliczeń.

## Użycie

1. **Inicjalizacja:** Aplikacja inicjalizuje silnik rozpoznawania mowy i obiekty syntezy podczas uruchamiania.

2. **Wejście:** Użytkownicy mogą wprowadzać liczby (0-9) i operatory matematyczne (plus, minus, mnożenie, dzielenie) za pomocą poleceń głosowych.

3. **Słowa Kluczowe Poleceń:**
   - `wykonaj`: Oblicza i wymawia wynik wprowadzonego wyrażenia matematycznego.
   - `zakończ`: Zamyka aplikację.
   - `wyczyść`: Czyści bieżące równanie.

4. **Przykładowe Użycie:**
   - Wymów liczbę, a następnie operator matematyczny, a potem kolejną liczbę.
   - Przykład: "Dwa plus trzy wykonaj" skutkuje wymówieniem odpowiedzi "Dwa plus trzy to pięć."

5. **Obsługa Błędów:**
   - Aplikacja udziela komunikatów o błędach dla nieprawidłowych równań lub prób dzielenia przez zero.

6. **Polecenie Pomocy:**
   - Użytkownicy mogą powiedzieć "pomoc", aby uzyskać informacje na temat korzystania z aplikacji.

## Struktura Kodu

Aplikacja jest zorganizowana jako konsolowa aplikacja C# z następującymi głównymi komponentami:

- **Inicjalizacja:** Metoda `InitializeRecognitionObjects` ustawia silnik rozpoznawania mowy i obiekty syntezy.

- **Obsługa Równania:** Metoda `UpdateEquation` przetwarza rozpoznane polecenia głosowe i aktualizuje bieżące równanie.

- **Obliczenia:** Metoda `ExecuteEquation` wykonuje obliczenia na podstawie wprowadzonego równania i wymawia wynik.

- **Pętla Główna:** Główna pętla aplikacji ciągle rozpoznaje polecenia głosowe, aż użytkownik zdecyduje się wyjść.

## Zależności

Aplikacja opiera się na bibliotece Microsoft Speech SDK do rozpoznawania mowy i syntezy. Upewnij się, że niezbędne biblioteki są zainstalowane dla poprawnego działania.

## Instrukcje Użycia

1. Skompiluj i uruchom aplikację.

2. Wypowiadaj polecenia zgodnie z podaną składnią i słowami kluczowymi.

3. Słuchaj wyników mówionych i postępuj zgodnie z udzielonymi wskazówkami.

## Autorzy

Ta aplikacja Kalkulatora Głosowego została opracowana przez [@Walu064].

