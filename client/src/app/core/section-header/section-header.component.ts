import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  currentPage:String;
constructor(private route:ActivatedRoute ){this.currentPage = this.route.snapshot.data['breadcrumb'];}
  ngOnInit(): void {
   
   console.log(this.route.snapshot.data['breadcrumb']);
  }
}
