Feature: BookingService

Scenario: When I create booking with valid data it is created
Given The following bookings exist
| HotelId | Start               | End                 |
| 1       | 2019-03-11Z00:00:00 | 2019-03-14Z00:00:00 |
When I add boooking for hotel 1 from 2019-03-15Z00:00:00 to 2019-03-20Z00:00:00
Then New booking added