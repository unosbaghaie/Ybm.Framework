import { Pipe } from 'angular2/core';
@Pipe({
    name: 'persianCurrency'
})
export class PersianCurrency {
    transform(value: number): string {
        return Intl.NumberFormat('fa-FA', { style: 'currency', currency: 'IRR' }).format(value);
    }
}