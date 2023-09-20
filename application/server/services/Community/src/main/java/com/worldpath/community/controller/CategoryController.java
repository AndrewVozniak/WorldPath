package com.worldpath.community.controller;

import com.worldpath.community.DTO.CategoryDTO;
import com.worldpath.community.service.CategoryService;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/category")
public class CategoryController {
    private final CategoryService categoryService;

    public CategoryController(CategoryService CategoryService) {
        this.categoryService = CategoryService;
    }

    /**
     * @Description Create category
     * @param categoryDTO CategoryDTO
     * @return CategoryDTO
     */
    @PostMapping("/")
    public ResponseEntity<CategoryDTO> createCategory(@RequestBody CategoryDTO categoryDTO) {
        return ResponseEntity.ok(categoryService.createCategory(categoryDTO));
    }
}
