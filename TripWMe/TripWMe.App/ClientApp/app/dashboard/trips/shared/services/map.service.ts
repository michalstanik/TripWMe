import { Injectable } from '@angular/core';

export class Country {
    totalArea: number;
    color: string;
}

export class Countries {
    Azerbaijan: Country;
}


let countries: Countries = {
    Azerbaijan: { totalArea: 1.08, color: "#B0C4DE" }
};

@Injectable()
export class MapService {
    getCountries(): Countries {
        return countries;
    }
}
