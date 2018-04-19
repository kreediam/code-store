import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { catchError, map, tap } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

export class DataService {

    constructor(
        private http: HttpClient,
        private urlFragment: string
    ) { }

    // todo: the 3 "gets" are a little repetitive
    // may want to refactor to have less duplicate code

    // todo: make sure this works. hasn't been tested yet.
    getById<T>(id: string): Observable<T> {

        let url = environment.apiBase + this.urlFragment;
        let options = {
            'id': id
        };

        return this.http.get<T>(url, { params: options }).pipe(
            tap(results => console.log(results)),
            catchError(this.handleError(null)));
    }

    // todo: make sure this works. hasn't been tested yet.
    getAll<T>(offset: number, limit: number): Observable<T[]> {

        let url = environment.apiBase + this.urlFragment + '/getAll';
        let options = {
            'offset': offset.toString(),
            'limit': limit.toString()
        };

        return this.http.get<T[]>(url, { params: options }).pipe(
            tap(results => console.log(results)),
            catchError(this.handleError([])));
    }

    getWhere<T>(where: string, offset: number = 0, limit: number = 20): Observable<T[]> {

        let url = environment.apiBase + this.urlFragment + '/getWhere';
        let options = {
            'whereClause': where,
            'offset': offset.toString(),
            'limit': limit.toString()
        };

        return this.http.get<T[]>(url, { params: options }).pipe(
            tap(results => console.log(results)),
            catchError(this.handleError([])));
    }

    post() {
        // todo: something
    }

    delete(id: string) {
        // todo: something
    }

    handleError<T>(result?: T) {
        return (error: any): Observable<T> => {
            console.log("API call failed: " + error);
            return of(result as T);
        };
    }
}
