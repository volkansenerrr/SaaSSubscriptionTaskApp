# SaaS Subscription & Payment Simulation System

## Proje Açıklaması
Bu proje, SaaS platformu için abonelik yönetimi ve ParamPos ödeme sistemi simülasyonu içeren bir ASP.NET Core MVC uygulamasıdır.

## Özellikler
- Abonelik paket seçimi (Standart / Premium)
- Dinamik fiyat hesaplama (AJAX + jQuery)
- Yıllık ödeme indirimi
- Ek özellik ekleme (kullanıcı, depolama)
- Kart BIN bazlı banka simülasyonu
- Taksit hesaplama sistemi
- SHA512 hash ile ödeme güvenlik simülasyonu
- Ödeme sonucu kaydı (MSSQL / EF Core)

## Kullanılan Teknolojiler
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- MSSQL
- jQuery / AJAX
- Bootstrap 5

## Ödeme Sistemi
ParamPos API simüle edilmiştir:
- BIN kontrolü
- Taksit hesaplama
- Hash (SHA512) oluşturma
- Callback mantığı simülasyonu

## Kurulum
1. Repository clone edilir
2. Connection string ayarlanır
3. Migration çalıştırılır
4. Proje çalıştırılır

## Not
Bu proje gerçek ödeme işlemi yapmaz, tamamen simülasyon amaçlıdır.
