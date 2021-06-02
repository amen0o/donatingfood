import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { User } from '../_models';

import { AccountService, AlertService } from '../_services';

@Component({ templateUrl: 'details.component.html' })
export class DetailsComponent implements OnInit {
    user: User;
    form: FormGroup;
    loading = false;
    submitted = false;
    intolerances = [];

    constructor(
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService,
        private formBuilder: FormBuilder
    ) { }

    ngOnInit(): void {
        this.user = this.accountService.userValue;

        this.form = this.formBuilder.group({
            foodIntolerances: this.formBuilder.array([])
        });

        this.accountService.getIntolerances()
            .pipe(first())
            .subscribe(x => {
                this.intolerances = x;

                for (let i = 0; i < x.length; i++) {
                    let userFi = this.user.foodIntolerances.find((fi: any) => {
                        
                        if (fi.id !== undefined) return fi.id == this.intolerances[i].id;
                        else return fi == this.intolerances[i].id;
                    });

                    this.intolerances[i].selected = userFi === undefined ? false : true;
                }
            })
    }

    buildIntolerances(): any {
        const arr = this.user.foodIntolerances.map((skill: any) => {
            return this.formBuilder.control(skill.selected);
        });

        return this.formBuilder.array(arr);
    }

    onCbChange(e) {
        const foodIntolerances: FormArray = this.form.get('foodIntolerances') as FormArray;

        if (e.target.checked) {
            foodIntolerances.push(new FormControl(e.target.value));
        } else {
            let i: number = 0;
            foodIntolerances.controls.forEach((item: FormControl) => {
                if (item.value == e.target.value) {
                    foodIntolerances.removeAt(i);
                    return;
                }
                i++;
            });
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        this.updateIntolerances();
    }

    private updateIntolerances() {
        this.accountService.update(this.user.id, this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Update successful', { keepAfterRouteChange: true });
                    this.router.navigate(['/']);
                },
                error => {
                    this.alertService.error(error);
                });
    }
}