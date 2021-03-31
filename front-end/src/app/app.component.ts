
import { Component, Injectable, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
@Injectable()

export class AppComponent implements OnInit {

	constructor(
		private route: ActivatedRoute,
		private router: Router
	) { }

  ngOnInit() { this.router.navigate(['/login']); }
  
}