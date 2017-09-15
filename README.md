# Psikoterapistler Burada
Kendi alanında uzmanlaşmış psikoterapistlerin, kendilerine, yine bu platform üzerinden sorulan soruları cevaplayabileceği bir web uygulaması.
* Platform üyelik üzerinden çalışır.
* Soru sorulduktan sonra, cevaplayacak kişiler seçilir.
* Seçilen kişiler kendilerine sorulan soruları kendi sayfalarında görür ve cevaplar.
* Login olmuş kişiler cevaplar için 'like', sorular için oy işlemi yapabilir. Böylece cevaplar ve sorular bir seçiciliğe sahip olur.
* Kişiler birbirini takip edebilir. Böylece takip edilen üyenin cevapları ve soruları bildirim olarak gelir.
* Takip, Like, Vote işlemlerinde bildirim oluşur.

Şunları kullandım:
  * Asp.Net MVC5
  * RestFul API
  * Mssql
  * Entity Framework Code First
  * Repository Pattern
  * Bootstrap
  * Asp web applicationda default olan template
  * DataTable
  * Underscore.js
  * BootBox.js
  * Jquery
  
  ## Mimari Yapı
     Proje iki katmandan oluşuyor. Database ile ilişkili olan Persistence Katmanı ve ilişkili olmayan Core Katmanı.
   #### Controller => Core <= Persistance 
   şeklinde bir bağımlılık diagramından söz edebiliriz. 
   Core katmanı Interface sınıflarını barındırıyor. Persistence ise bu interfaceleri tanımladığım sınıfları içeriyor.
   Controller DBContext bağımlılığını azaltmak için UnitOfWork sınıfını kullandım. 
   Fakat controller high-level bir katman olduğu halde low-level bir katman olan UnitOfWork ile tightly coupled oluşturuyordu. Bunun için de IUnitOfWork sınıfını kullandım. 
   IUnitOfWork IRepository'leri içeren tamamen Abstract bir sınıfı tanımlıyor. Daha sonra UnitOfWork sınıfını IUnitOfWork sınıfına bağımlı hale getirdim. Yine aynı şekilde Controller katmanı ile IUnitOfWork arasında bir bağımlılık oluşturdum.
   
   #### Controller => IUnitOfWork <= UnitOfWork

Artık Controller hig-level katmanı bir Abstract sınıfa bağımlı hale geldi. Yine aynı şekilde, low-level ve detay sınıf olan UnitOfWork'da Abstrack bir sınıfa bağımlı hale geldi. Aslında yaptığım şey Core Katmanı tamamen bağımsız bir hale getirmek oldu. Uygulamanın test edilebilirliği artmış oldu. Ayrıca Core Katamanı ORM Frameworkten bağımsız bir yapıya kavuştu. UnitOfWork'te yapılacak değişiklik IUnitOfWork Katmanını etkilememiş olacak.

Diğer tarafta uygulamada UnitOfWork Katmanında hala bir DbContext bağımlılığı da devam ediyordu. Bu da dolaylı yoldan Controller - DbContext tightly coupled problemine sebep oluyordu. Bunu çözmek içinde bir Dependency Injection Framework'ü kullandım. (Ninject 3.2.1.0)
   
