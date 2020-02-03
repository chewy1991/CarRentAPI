using System;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using CarRentApi.Repositories;


namespace CarRentRestAPI.Demo
{
    public class DemoInitializer
    {
        public IRepository<CarClass> ClassRepository { get; }
        public IRepository<CarBrand> BrandRepository { get; }
        public IRepository<CarType> TypeRepository { get; }
        public IRepository<Car> CarRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Reservation> ReservationRepository { get; }
        public IRepository<Contract> ContractRepository { get; }

        public DemoInitializer(
            IRepository<CarClass> classRepository,
            IRepository<CarBrand> brandRepository,
            IRepository<CarType> typeRepository,
            IRepository<Car> carRepository,
            IRepository<Customer> customerRepository,
            IRepository<Reservation> reservationRepository,
            IRepository<Contract> contractRepository)
        {
            ClassRepository = classRepository;
            BrandRepository = brandRepository;
            TypeRepository = typeRepository;
            CarRepository = carRepository;
            CustomerRepository = customerRepository;
            ReservationRepository = reservationRepository;
            ContractRepository = contractRepository;
        }

        public async Task InitDemoDataAsync()
        {
            var brands = await BrandRepository.GetAllAsync();
            if (brands.Any())
                return; // already initialized

            var brandA = new CarBrand
            {
                BrandName = "Audi"
            };

            var brandB = new CarBrand
            {
                BrandName = "BMW"
            };

            var brandC = new CarBrand
            {
                BrandName = "Fiat"
            };

            brandA = await BrandRepository.AddAsync(brandA);
            brandB = await BrandRepository.AddAsync(brandB);
            brandC = await BrandRepository.AddAsync(brandC);

            var classA = new CarClass
            {
                Class = "Einfachklasse",
                CostsPerDay = 102.20M
            };

            var classB = new CarClass
            {
                Class = "Mittelklasse",
                CostsPerDay = 180.50M
            };

            var classC = new CarClass
            {
                Class = "Luxusklasse",
                CostsPerDay = 250.99M
            };

            classA = await ClassRepository.AddAsync(classA);
            classB = await ClassRepository.AddAsync(classB);
            classC = await ClassRepository.AddAsync(classC);

            var typeA = new CarType
            {
                carType = "Kleinwagen"
            };

            var typeB = new CarType
            {
                carType = "Kombi"
            };

            var typeC = new CarType
            {
                carType = "SUV"
            };

            typeA = await TypeRepository.AddAsync(typeA);
            typeB = await TypeRepository.AddAsync(typeB);
            typeC = await TypeRepository.AddAsync(typeC);

            var customerA = new Customer
            {
                Firstname = "Peter",
                Lastname = "Müller",
                Adress = "Mordorweg 4, 8355 Gondor",
                EMailAdress = "peter.mueller@gondor.ch",
                Telephonenumber = "079 546 65 65"
            };

            var customerB = new Customer
            {
                Firstname = "Maria",
                Lastname = "Meier",
                Adress = "Rohangasse 23, 5564 Auenland",
                EMailAdress = "maria.meier@auenland.ch",
                Telephonenumber = "76 215 54 64"
            };

            var customerC = new Customer
            {
                Firstname = "Bruno",
                Lastname = "Gander",
                Adress = "Isengardweg 3, 5445 Helmsklamm",
                EMailAdress = "bruno.gander@helmsklamm.ch",
                Telephonenumber = "76 651 12 35"
            };

            customerA = await CustomerRepository.AddAsync(customerA);
            customerB = await CustomerRepository.AddAsync(customerB);
            customerC = await CustomerRepository.AddAsync(customerC);

            var carA = new Car
            {
                BrandId = brandA.Id,
                ClassId = classA.Id,
                horsepower = 112,
                kilometer = 216535,
                CostsPerDay = classA.CostsPerDay,
                RegistrationYear = 2000,
                TypeId = typeA.Id
            };

            var carB = new Car
            {
                BrandId = brandB.Id,
                ClassId = classB.Id,
                horsepower = 212,
                kilometer = 116535,
                CostsPerDay = classB.CostsPerDay,
                RegistrationYear = 2010,
                TypeId = typeB.Id
            };

            var carC = new Car
            {
                BrandId = brandC.Id,
                ClassId = classC.Id,
                horsepower = 312,
                kilometer = 16535,
                CostsPerDay = classC.CostsPerDay,
                RegistrationYear = 2018,
                TypeId = typeC.Id
            };

            carA = await CarRepository.AddAsync(carA);
            carB = await CarRepository.AddAsync(carB);
            carC = await CarRepository.AddAsync(carC);

            var reservationA = new Reservation
            {
                CarId = carA.Id,
                CustomerId = customerA.Id,
                RentalDays = 12,
                Costs = carA.CostsPerDay * 12,
                RentalDate = DateTime.Now.AddDays(12),
                ReservationDate = DateTime.Now,
                State = ReservationState.pending
            };

            var reservationB = new Reservation
            {
                CustomerId = customerB.Id,
                CarId = carB.Id,
                RentalDays = 2,
                Costs = carB.CostsPerDay * 2,
                RentalDate = DateTime.Now.AddDays(1),
                ReservationDate = DateTime.Now.AddDays(-3),
                State = ReservationState.reserved
            };

            var reservationC = new Reservation
            {
                CustomerId = customerC.Id,
                CarId = carC.Id,
                RentalDays = 42,
                Costs = carC.CostsPerDay * 42,
                RentalDate = DateTime.Now.AddDays(-2),
                ReservationDate = DateTime.Now.AddDays(-8),
                State = ReservationState.contracted
            };

            await ReservationRepository.AddAsync(reservationA);
            await ReservationRepository.AddAsync(reservationB);
            reservationC = await ReservationRepository.AddAsync(reservationC);

            var contractA = new Contract()
            {
                ReservationId = reservationC.Id,
            };

            await ContractRepository.AddAsync(contractA);

        }
    }
}
