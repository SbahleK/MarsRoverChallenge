import {
	HttpClient
} from '@angular/common/http';
import {
	Component
} from '@angular/core';
import {
	environment
} from 'src/environments/environment';

export interface IRoverCommand {
	plateau: string;
	rovers: IRover[];
}

export interface IRover {
	position: string;
	movement: string;
}
@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent {
	Plateau: string;
	Position: string;
	Movements: string;
	roverList: IRover[] = [];
	rovers: string[] = [];
	show: boolean = false;
	showResults: boolean = false;
	disabled: boolean = true;

	validPosition: boolean = true;
	validMovement: boolean = true;
	validPlateau: boolean = true;

	constructor(private http: HttpClient) {}

	addRover() {
		this.validatePlateau();
		if (this.validPlateau) {
			this.show = true;
		}
	}

	Deploy() {
		this.rovers = [];
		var command: IRoverCommand = {
			plateau: this.Plateau,
			rovers: this.roverList
		};

		var headers = {
			'content-type': 'application/json'
		}
		var body = JSON.stringify(command);
		var url = environment.apiEndpoint + '/Rover/SendCommands';
		this.http.post < string[] > (url, body, {
			headers: headers
		}).subscribe(data => {
			data.forEach(item => {
				this.rovers.push(item);
			});
		});
	}

	validatePosition() {
		let regexpPosition = new RegExp('^\\d+ \\d+ [NSWE]$');
		this.validPosition = regexpPosition.test(this.Position);
	}

	validatePlateau() {
		let regexpPlateau = new RegExp('^\\d+ \\d+');
		this.validPlateau = regexpPlateau.test(this.Plateau);
	}

	validateMovement() {
		let regexpMovement = new RegExp('^[LMR]+$');
		this.validMovement = regexpMovement.test(this.Movements);
	}

	onclick() {
		if (this.Position.length > 0 && this.Movements.length > 0) {
			this.validatePosition();
			this.validateMovement();
			if (this.validPosition && this.validMovement) {
				this.roverList.push({
					position: this.Position,
					movement: this.Movements
				});
				this.Position = '';
				this.Movements = '';
				this.show = false;
				this.disabled = false;
			}
		}
	}

	ondelete(deleteme) {
		this.roverList.splice(deleteme, 1)
		if (this.roverList.length === 0) {
			this.disabled = true;
		}
	}
}