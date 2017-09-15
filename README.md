# Psikoterapistler Burada
Kendi alanında uzmanlaşmış psikoterapistlerin, kendilerine, yine bu platform üzerinden sorulan soruları cevaplayabileceği bir web uygulaması.
* Platform üyelik üzerinden çalışır.
* Soru sorulduktan sonra, cevaplayacak kişiler seçilir.
* Seçilen kişiler kendilerine sorulan soruları kendi sayfalarında görür ve cevaplar.
* Login olmuş kişiler cevaplar için 'like', sorular için oy işlemi yapabilir. Böylece cevaplar ve sorular bir seçiciliğe sahip olur.
* Kişiler birbirini takip edebilir. Böylece takip edilen üyenin cevapları ve soruları bildirim olarak gelir.
* Takip, Like, Vote işlemlerinde bildirim oluşur.

__Şunları kullandım:__
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
     Proje iki katmandan oluşuyor. Database ile ilişkili olan __Persistence Katmanı__ ve ilişkili olmayan __Core Katmanı__.
   #### Controller => Core <= Persistance 
   şeklinde bir bağımlılık diagramından söz edebiliriz. 
   _Core Katmanı_ Interface sınıflarını barındırıyor. _Persistence_ ise bu interfaceleri tanımladığım sınıfları içeriyor.
   Controller tarafında, _Controller DBContext_ bağımlılığını azaltmak için __UnitOfWork__ sınıfını kullandım. 
   Fakat controller _high-level_ bir katman olduğu halde _low-level_ bir katman olan UnitOfWork ile tightly coupled oluşturuyordu. Bunun için de __IUnitOfWork__ sınıfını kullandım. 
   _IUnitOfWork_ IRepository'leri içeren tamamen Abstract bir sınıfı tanımlıyor. Daha sonra _UnitOfWork_ sınıfını _IUnitOfWork_ sınıfına bağımlı hale getirdim. Yine aynı şekilde _Controller_ katmanı ile _IUnitOfWork_ arasında bir bağımlılık oluşturdum.
   
   #### Controller => IUnitOfWork <= UnitOfWork

Artık _Controller_ hig-level katmanı bir Abstract sınıfa bağımlı hale geldi. Yine aynı şekilde, low-level ve detay sınıf olan _UnitOfWork'da_ Abstrack bir sınıfa bağımlı hale geldi. Aslında yaptığım şey _Core Katmanı_ tamamen bağımsız bir hale getirmek oldu. Uygulamanın test edilebilirliği artmış oldu. Ayrıca Core Katamanı ORM Frameworkten bağımsız bir yapıya kavuştu. UnitOfWork'te yapılacak değişiklik IUnitOfWork Katmanını etkilememiş olacak.

Diğer tarafta uygulamada _UnitOfWork Katmanında_ _DbContext_ bağımlılığı hala devam ediyordu. Bu da dolaylı yoldan _Controller - DbContext_ tightly coupled problemine sebep oluyordu. Bunu çözmek içinde bir _Dependency Injection Framework'ü_ kullandım. (Ninject 3.2.1.0)
   
