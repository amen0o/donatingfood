import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { ProductService } from '../_services';
import { environment } from '../../environments/environment';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    products = null;

    constructor(private productService: ProductService) { }

    ngOnInit() {
        this.productService.getAll()
            .pipe(first())
            .subscribe(products => {
                this.products = products;
            });
    }

    deleteProduct(id: string) {
        const product = this.products.find(x => x.id === id);
        product.isDeleting = true;
        this.productService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.products = this.products.filter(x => x.id !== id);
            });
    }
}