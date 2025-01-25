# ChefConnect

## Struktura projektu

- `/backend` - zawiera folder dla każdego mikroserwisu. W każdym takim folderze znajduje się:
    - kod źródłowy w języku Java
    - plik Dockerfile do budowania obrazu mikroserwisu
- `/frontend-mobile` - zawiera kod źródłowy aplikacji mobilnej napisanej w .NET przy wykorzystaniu frameworka MAUI
- `/frontend-web/chef-connect` - zawiera kod źródłowy frontendu napisanego w Angular
- `/frontend-web/e2e` - zawiera skonfigurowany projekt w Cypress i test integracyjny E2E wybranego wycinka naszego systemu
- `/kubernetes` - zawiera konfiguracje, wykorzystywane przez kubernetesa, do deploymentu poszczególnych mikroserwisów. Dla każdego mikroserwisu jest stworzony plik `yml`
- `/terraform-basic-infra` - zawiera terraform do budowy podstawowej infrastruktury (tworzenie klastra i node group w EKS, sieci VPC, baz RDS, Cognito, S3 Bucket)
- `/terraform-gateway` - zawiera terraform do tworzenia API Gateway oraz tworzenia ścieżek do stworzonych mikroserwisów
- `/terraform-jenkins` - zawiera terraform do tworzenia osobnego VPC i EC2 na których znajduje się: serwer jenkins oraz worker node do wykonywania buildów

## Zrealizowane cele

### Frontend
- Integracja z AWS Cognito
    - Uwierzytelnianie za pomocą adresu e-mail i hasła
    - Autoryzacja po stronie mikroserwisów za pomocą przekazywanego tokenu
    - Wykorzystanie przypisanych grup użytkownika do zabezpieczenia widoków interfejsu użytkownika
- Integracja z AWS-Amplify/ui-angular component
    - Wykorzystanie komponentu do logowania i tworzenia konta użytkownika, zgodnego z naszym modelem danych
    - Integracja z systemem do potwierdzania rejestracji za pomocą kodu wysłanego na adres e-mail
    - Wykorzystanie serwisu do komunikacji z AWS Cognito, potwierdzania i odświeżania tokenów oraz do wylogowywania
- Stworzenie 3 przykładowych paneli dla każdej z dostępnych ról
    - Panel administratora
    - Panel pracownika restauracji
    - Panel menedżera
        - Użyto mechanizmów Guards dla zabezpieczenia widoków ze względu na role użytkownika
- Integracja z RestaurantService i ReservationService dla Pracownika restauracji
    - Zaprezentowanie przepływu zdarzeń dla przypadku użycia obejmującego rezerwacje stolika w restauracji na dany dzień.
        - Klient restauracji dokonuje rezerwacji stolika
        - Pracownik restauracji widzi nową niepotwierdzoną rezerwację.
        - Pracownik restauracji zaznacza nową rezerwację, jest w stanie wybrać dostępne stoliki, potwierdzić rezerwację, a na końcu jest powiadamiany o pomyślnym potwierdzeniu rezerwacji
        - Klient restauracji widzi zmianę statusu rezerwacji
        - Po ponownej prośbie o rezerwację na daną godzinę
            - Klient aplikacji mobilnej widzi mniejszą dostępną liczbę miejsc
            - Pracownik który potwierdza kolejną rezerwację nie może wybrać zarezerwowanych stolików
- Integracja z RestaurantService dla Administratora i testy integracyjne
    - Integracja z RestaurantService dla panelu administratora została zaprezentowana poprzez uruchomienie testu integracyjnego E2E
    - Przypadek testowy został napisany w narzędziu Cypress
        - Obejmował otworzenie strony głównej
        - Zalogowanie się na konto administratora
        - Wyświetleniu danych restauracji pobranych z RestaurantService
        - Wylogowaniu się użytkownika
    - Zaprezentowany przypadek testowy przebiegł pomyślnie
    - Dodatkowo jako jedno z wymagań niefunkcjonalnych test E2E został uruchomiony na dwóch różnych silnikach przeglądarek, w naszym przykładzie na przeglądarce Chrome i Firefox, gdzie oba testy przebiegły pomyślnie
    - Zgodnie z deklaracją na diagramie rozmieszczenia aplikacja webowa została wdrożona w formie plików renderowanych po stronie klienta, udostępnianych i przechowywanych w serwisie S3.
    - Dla zapewnienia częściowej automatyzacji procesu wdrożenia został utworzony skrypt wdrożeniowy, budujący aplikację i wysyłający gotowe pliki do bucketu. Dzięki temu nowa wersja aplikacji jest dostępna dla użytkowników po wywołaniu jednej komendy
    - Dla zapewnienia spójności wyglądowej pomiędzy widokami została wykorzystana biblioteka komponentów Angular Material z użyciem niestandardowego motywu, pozwalająca na dowolne kolorystyczne stylowanie aplikacji

### Aplikacja mobilna

- Zaimplementowanie widoków do wszystkich wymagań funkcjonalnych:
    - Autoryzacji użytkownika (widok logowania, rejestracji, widok do potwierdzenia konta) 
    - Zarządzanie danymi klienta (widok edycji hasła, widok edycji danych konta, widok szczegółów konta)
    - Przeglądanie listy restauracji (wraz z zapisywaniem wyboru ulubionej restauracji)
    - Widok dokonywania rezerwacji, wraz z wyborem daty, godziny, liczby miejsc
    - Widok listy dokonanych rezerwacji
    - Panel do dodawania opinii o odbytych wizytach w restauracji
- Integracja z Cognito
    - Uwierzytelnianie za pomocą adresu e-mail i hasła
    - Modyfikacja danych konta bezpośrednio w Cognito
    - Potwierdzanie konta poprzez kod przychodzący na adres email
- Integracja z mikroserwisami
    - Komunikacja z mikroserwisami za pomocą ścieżki w API Gateway
    - Autoryzacja po stronie mikroserwisów za pomocą przekazywanego tokenu
    - Integracja z restaurants-service
        - Pobieranie listy restauracji jak i podstawowych danych o poszczególnych restauracjach (liczba stolików, godziny otwarcia, adres itp.)
        - Zapisywanie ulubionej restauracji dla danego użytkownika
    - Integracja z reservations-service
        - Pobieranie listy rezerwacji wraz z ich statusami
        - Pobieranie dostępnych time slotów przy dokonywaniu rezerwacji wraz z liczbą dostępnych miejsc
        - Odwoływanie rezerwacji
        - Tworzenie rezerwacji
        - Zapisywanie opinii o odbytych rezerwacjach
        - Pobieranie średniej oceny danej restauracji
- Testy jednostkowe dla serwisów integrujących się z backendem
- Multi-platformowość
    - Pisanie zgodnie ze standardami MAUI, zapewniającymi multi-platformowość. Aplikacja powinna zadziałać na iOS, lecz nie byliśmy w stanie tego sprawdzić (nie mieliśmy MacBooka, iPhone + koszty uzyskania licencji na platformie Apple)

### Mikroserwisy

#### Restaurants-Service
- Integracja z Cognito w celu obsługi uwierzytelnienia w API z użyciem OAuth2 i JWT
- Integracja z RDS
- Obraz mikroserwisu udostępniony na DockerHub i wykorzystywany przez kontener w podzie Kubernetes'a
- Dodane endpointy:
    - ogólnego przeznaczenia:
        - Pobieranie danych wszystkich restauracji
        - Pobieranie danych konkretnej restauracji
        - Pobieranie informacji o stolikach we wszystkich restauracjach
        - Pobieranie informacji o stolikach w konkretnej restauracji
    - klienta:
        - Pobieranie listy ulubionych restauracji
        - Dodawanie restauracji do ulubionych
        - Usuwanie restauracji z ulubionych
#### Reservations-Service
- Integracja z Cognito w celu obsługi uwierzytelnienia w API z użyciem OAuth2 i JWT
- Integracja z RDS
- Integracja z systemem kolejkowym SQS
    - Wysyłanie dokonanych przez klienta rezerwacji na kolejkę (które może odebrać następnie z kolejki mikroserwis Analytics-Service)
- Obraz mikroserwisu udostępniony na DockerHub i wykorzystywany przez kontener w podzie Kubernetes'a
- Integracja z mikroserwisem Restaurant-Service. Pobieranie istotnych i szczegółowych danych potrzebnych w niektórych funkcjonalnościach
- Dodane endpointy dla klienta
    - Tworzenie rezerwacji. Możliwość wyboru restauracji, daty i godziny oraz ilość osób dla rezerwacji
        - Brak możliwości dodania rezerwacji w przeszłości
    - Możliwość podglądu swoich rezerwacji
    - Możliwość anulowania rezerwacji
        - Brak możliwości anulowania nieistniejącej rezerwacji
    - Podgląd dostępności stolików w restauracji w danym dniu
    - Możliwość dodania opinii dla rezerwacji (ocena w skali od 1 do 5) wraz z opisem
        - Brak możliwości dodania opinii dla nieistniejącej rezerwacji
        - Brak możliwości dodania więcej niż jednej opinii dla pojedynczej rezerwacji
    - Możliwość podglądu średniej oceny restauracji
- Dodane endpointy dla pracownika restauracji
    - Pobranie wszystkich rezerwacji dla danej restauracji, w której pracuje pracownik
    - Pobranie wszystkich dostępnych stolików w restauracji dla danej wybranej godziny rezerwacji
    - Potwierdzanie rezerwacji klienta. Pracownik wybiera stoliki w restauracji i przypisuje je do rezerwacji
        - Sprawdzanie, czy liczba miejsc przy stolikach, które wybrał pracownik nie jest mniejsza niż wymagana ilość miejsc podana przez klienta w utworzonej przez siebie rezerwacji
        - Brak możliwości rezerwacji niedostępnego stolika


#### Analytics-Service
- Integracja z Cognito w celu obsługi uwierzytelnienia w API z użyciem oAuth2 i JWT
- Integracja z RDS
- Integracja z systemem kolejkowym SQS
    - Obsługa zdarzeń o dokonaniu rezerwacji
- Obraz mikroserwisu udostępniony na DockerHub i wykorzystywany przez kontener w podzie Kubernetes'a
- Dodany endpoint do pobierania raportu o rezerwacjach zawierającego podsumowanie ilości dokonanych rezerwacji w każdej godzinie w wybranym okresie czasu.

### Infrastruktura

#### Infrastruktura Systemu
- Wykorzystanie terraform do automatyzacji procesu deploymentu.
    - Wykorzystanie mechanizmu modułów, w celu ograniczenia duplikacji kodu dotyczącego powtarzalnych zasobów takich jak RDS i integracji mikroserwisów z API Gateway
- Wykorzystanie infrastruktury AWS, oraz serwisów:
    - Relational Database Service (RDS)
        - Dla każdego mikroserwisu są stworzone osobne instancje bazy danych
        - Każda instancja jest umieszczona w prywatnej podsieci
    - Cognito User Pool
        - Definicja struktury danych zgodnych z modelem systemu
        - Definicja polityki haseł
        - Definicja struktury wiadomości do potwierdzania konta w systemie
        - Definicja grup użytkowników
        - Deklaracja OAuth2 Authentity Provider (User Pool Client)
    - Simple Queue Service (SQS)
    - VPC
        - VPC subnets
            - Prywatne podsieci dla instancji (tworzony przez Kubernetesa) mikroserwisów. Instancje znajdują się w tem samej podsieci prywatnej co baza danych dla danego mikroserwisu. 
            - Dla podsieci mamy również skonfigurowane tabele routingu, umożliwiające odpowiednią komunikację
        - VPC Internet Gateway
        - VPC Elastic IP
            - Stały, publiczny adres IP do umożliwienia komunikacji z endpointami naszych mikroserwisów
        - VPC NAT Gateway
            - Brama NAT do umożliwienia komunikacji mikroserwisów z publicznym internetem (z klientami)
    - Elastic Kubernetes Service (EKS)
        - Cluster
        - Node Groupy
        - Load Balancery
            - Przekierowuje ruch przychodzący z api gateway do odpowiedniego, najmniej obciążonego poda, działającego w ramach serwisu.
    - S3
        - Bucket do hostowania aplikacji webowej
    - API Gateway
        - Api gateway przekierowuje ruch przychodzący z internetu do odpowiedniego load balancera dla danego mikroserwisu. 

#### Jenkins

- Osobna konfiguracja terraform do infrastruktury odpowiedzialnej za CI/CD.
- Wykorzystanie osobnej sieci VPC, wraz z security groupami. Infrastruktura systemu i środowisko CI/CD znajdują się na kompletnie odosobnionych zasobach
- Instancja EC2 dla serwera Jenkins
    - Automatyczna instalacja, przy pomocy Ansible, wszystkich niezbędnych narzędzi do działania serwera Jenkins oraz jego wstępnej inicjalizacji (aktywowanie konta administratora)
- Instancje EC2 dla worker node:
    - Automatyczne tworzenie instancji w AWS
    - Automatyczna instalacja potrzebnych narzędzi (Kubernetes, AWS CLI, plików potrzebnych do komunikacji z serwerem Jenkins) z wykorzystaniem Ansible
- Konfiguracja WebHooka w GitHubie do wysyłania triggera do Jenkinsa przy wykonaniu commita na branchu main
- Konfiguracja pipelinów w jenkins
    - Pipeliny do automatycznego przebudowania i wypushowania obrazu, do Docker Hub, dla każdego mikroserwisu. Poszczególne pipeline triggerują się tylko dla zmian w danym mikroserwisie.
    - Pipeliny do automatycznego deploymentu, wykorzystujący Kubernetes CLI do wymuszenia pobrania nowego obrazu w danym mikroserwisie. Triggerowany po pomyślnym wykonaniu Build (poprzedniego pipeline)


### Niezrealizowane cele

- Orders-Service, Inventory-Service - ze względu na brak czasu oraz skupienie się na zaimplementowaniu w całości jednego większego feature (rezerwacji), nie zdążyliśmy zaimplementować tych dwóch serwisów oraz wymagań funkcjonalnych związanych z nimi
- Pełna integracja z Analytics-Service - zdążyliśmy zaimplementować analytics-service oraz zintegrować go z innymi mikroserwisami (reservations-service), tak aby wysyłały dane do kolejki SQS, natomiast nie zdążyliśmy zintegrować analitiki z naszym frontendem webowym. Planowaliśmy wyświetlać wykresy oraz statystyki dla menadżera
- Integracja z zewnętrznymi systemami (płatności, Google Distance API, hurtownia)
- Wykonywanie backupu dla RDS i przechowaynie kopii na S3
- Wykorzystanie ECR do przechowywania obrazu dockerowych (wykorzystywaliśmy Docker Hub)
- Skonfigurowanie CloudTrail do logowania wszystkich zdarzeń w infrastrukturze (też byliśmy mocno ograniczeni przez uprawnienia w naszym koncie studenckim na AWS)
