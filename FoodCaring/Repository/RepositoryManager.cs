using System.Threading.Tasks;
using Entities;

namespace Repository
{
    public class RepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ProductRepository _productRepository;
        private RestaurantRepository _restaurantRepository;
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemRepository;
        private FoodIntoleranceRepository _foodIntoleranceRepository;

        public RepositoryManager(RepositoryContext repositoryContext) 
        {
            _repositoryContext = repositoryContext;
        }

        public ProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public RestaurantRepository Restaurant
        {
            get
            {
                if (_restaurantRepository == null)
                    _restaurantRepository = new RestaurantRepository(_repositoryContext);

                return _restaurantRepository;
            }
        }

        public OrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_repositoryContext, OrderItem);

                return _orderRepository;
            }
        }

        public OrderItemRepository OrderItem
        {
            get
            {
                if (_orderRepository == null)
                    _orderItemRepository = new OrderItemRepository(_repositoryContext);

                return _orderItemRepository;
            }
        }

        public FoodIntoleranceRepository FoodIntolerance
        {
            get
            {
                if (_foodIntoleranceRepository == null)
                    _foodIntoleranceRepository = new FoodIntoleranceRepository(_repositoryContext);

                return _foodIntoleranceRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
