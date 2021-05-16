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
                    _orderRepository = new OrderRepository(_repositoryContext);

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

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
